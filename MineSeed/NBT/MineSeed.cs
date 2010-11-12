using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LibNbt;
using LibNbt.Tags;

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

        public static void Set(string input, string file)
        {
            byte[] XOR_KEY = { 108, 111, 108, 32, 101, 97, 115, 116, 101, 114, 32, 101, 103, 103 };

            if (!input.StartsWith("[") || !input.EndsWith("]"))
                return;
            input = input.Replace("[", "").Replace("]", "");
            byte[] data = Convert.FromBase64String(input);

            for (int i = 0; i < data.Length; i++)
            {
                int mod = i % XOR_KEY.Length;
                data[i] = (byte)((int)data[i] ^ (int)XOR_KEY[mod]);
            }

            long randomSeed = BitConverter.ToInt64(data, 0);
            long time = BitConverter.ToInt64(data, sizeof(long));

        }

    }
}
