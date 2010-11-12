using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibNbt;
using LibNbt.Tags;
using System.Windows.Forms;
namespace MineSeed
{
    class MineSeeder
    {
        

        public static string Get(string world_file)
        {
            byte[] XOR_KEY = { 108, 111, 108, 32, 101, 97, 115, 116, 101, 114, 32, 101, 103, 103 };

            NbtFile nbt = new NbtFile(world_file, true);
            nbt.LoadFile();
            
            byte[] randSeed = BitConverter.GetBytes(nbt.RootTag.Tags[0].Query<NbtLong>("/Data/RandomSeed").Value);
            byte[] time = BitConverter.GetBytes(nbt.RootTag.Tags[0].Query<NbtLong>("/Data/Time").Value);

            byte[] playerX = BitConverter.GetBytes((int)nbt.RootTag.Tags[0].Query<NbtDouble>("/Data/Player/Pos/0").Value);
            byte[] playerY = BitConverter.GetBytes((int)nbt.RootTag.Tags[0].Query<NbtDouble>("/Data/Player/Pos/1").Value);
            byte[] playerZ = BitConverter.GetBytes((int)nbt.RootTag.Tags[0].Query<NbtDouble>("/Data/Player/Pos/2").Value);

            byte[] rotationX = BitConverter.GetBytes(nbt.RootTag.Tags[0].Query<NbtFloat>("/Data/Player/Rotation/0").Value);
            byte[] rotationY = BitConverter.GetBytes(nbt.RootTag.Tags[0].Query<NbtFloat>("/Data/Player/Rotation/1").Value);

            uint size = (sizeof(long) * 2) + (sizeof(int) * 3) + (sizeof(float) * 2);

            byte[] data = new byte[size];

            int end = 0;
            
            Array.Copy(randSeed, 0, data, end, randSeed.Length);
            end += randSeed.Length;
            Array.Copy(time, 0, data, end, time.Length);
            end += time.Length;
            Array.Copy(playerX, 0, data, end, playerX.Length);
            end += playerX.Length;
            Array.Copy(playerY, 0, data, end, playerY.Length);
            end += playerY.Length;
            Array.Copy(playerZ, 0, data, end, playerZ.Length);
            end += playerZ.Length;
            Array.Copy(rotationX, 0, data, end, rotationX.Length);
            end += rotationX.Length;
            Array.Copy(rotationY, 0, data, end, rotationY.Length);

            for (int i = 0; i < data.Length; i++)
            {
                int mod = i % XOR_KEY.Length;
                data[i] = (byte)((int)data[i] ^ (int)XOR_KEY[mod]);
            }

            return "[" + Convert.ToBase64String(data) + "]";
        }

        public static bool Set(string input, string file)
        {
            try
            {
                byte[] XOR_KEY = { 108, 111, 108, 32, 101, 97, 115, 116, 101, 114, 32, 101, 103, 103 };

                if (!input.StartsWith("[") || !input.EndsWith("]"))
                    return false;
                input = input.Replace("[", "").Replace("]", "");
                byte[] data = Convert.FromBase64String(input);

                for (int i = 0; i < data.Length; i++)
                {
                    int mod = i % XOR_KEY.Length;
                    data[i] = (byte)((int)data[i] ^ (int)XOR_KEY[mod]);
                }

                int end = 0;

                long randomSeed = BitConverter.ToInt64(data, 0);
                end += sizeof(long);

                long time = BitConverter.ToInt64(data, end);
                end += sizeof(long);

                int playerX = BitConverter.ToInt32(data, end);
                end += sizeof(int);

                int playerY = BitConverter.ToInt32(data, end);
                end += sizeof(int);

                int playerZ = BitConverter.ToInt32(data, end) + 1;
                end += sizeof(int);

                float rotationX = BitConverter.ToSingle(data, end);
                end += sizeof(float);

                float rotationY = BitConverter.ToSingle(data, end);

                NbtFile nbt = new NbtFile();

                NbtCompound compound = new NbtCompound();

                NbtCompound data_compound = new NbtCompound("Data");

                data_compound.Tags.Add(new NbtLong("RandomSeed", randomSeed));
                data_compound.Tags.Add(new NbtLong("Time", time));
                data_compound.Tags.Add(new NbtInt("SpawnX", playerX));
                data_compound.Tags.Add(new NbtInt("SpawnY", playerY));
                data_compound.Tags.Add(new NbtInt("SpawnZ", playerZ));

                NbtCompound player_compound = new NbtCompound("Player");

                player_compound.Tags.Add(new NbtByte("OnGround", 1));
                player_compound.Tags.Add(new NbtShort("Air", 300));
                player_compound.Tags.Add(new NbtShort("AttackTime", 0));
                player_compound.Tags.Add(new NbtShort("DeathTime", 0));
                player_compound.Tags.Add(new NbtShort("Fire", -20));
                player_compound.Tags.Add(new NbtShort("Health", 20));
                player_compound.Tags.Add(new NbtShort("HurtTime", 0));
                player_compound.Tags.Add(new NbtInt("Dimension", 0));
                player_compound.Tags.Add(new NbtInt("Score", 0));
                player_compound.Tags.Add(new NbtShort("FallDistance", 0));
                

                NbtList player_pos = new NbtList("Pos");
                player_pos.Tags.Add(new NbtDouble("", (double)playerX));
                player_pos.Tags.Add(new NbtDouble("", (double)playerY));
                player_pos.Tags.Add(new NbtDouble("", (double)playerZ));
                player_compound.Tags.Add(player_pos);

                NbtList player_rot = new NbtList("Rotation");
                player_rot.Tags.Add(new NbtFloat("", rotationX));
                player_rot.Tags.Add(new NbtFloat("", rotationY));
                player_compound.Tags.Add(player_rot);

                NbtList inv_compound = new NbtList("Inventory");
                NbtCompound item_compound = new NbtCompound("");
                item_compound.Tags.Add(new NbtByte("Count", 18));
                item_compound.Tags.Add(new NbtByte("Slot", 0));
                item_compound.Tags.Add(new NbtInt("Damage", 0));
                item_compound.Tags.Add(new NbtInt("id", 12));
                inv_compound.Tags.Add(item_compound);
                player_compound.Tags.Add(inv_compound);


                NbtList player_mot = new NbtList("Motion");
                player_mot.Tags.Add(new NbtDouble("", 0.0));
                player_mot.Tags.Add(new NbtDouble("", 0.0));
                player_mot.Tags.Add(new NbtDouble("", 0.0));
                player_compound.Tags.Add(player_mot);


                data_compound.Tags.Add(player_compound);
                nbt.RootTag = compound;
                nbt.RootTag.Tags.Add(data_compound);

                Stream stream = File.OpenWrite(file);
                nbt.SaveFile(stream);
                stream.Close();
                stream.Dispose();

                return true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            
            
            
        }

    }
}
