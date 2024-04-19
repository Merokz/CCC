using CCC.Helpers;

namespace CCC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunLevel(4, 2, true);
        }
        public static void RunLevel(int level, int specificLevel = -1, bool debug = false)
        {
            string currentDir = Directory.GetCurrentDirectory();
            currentDir = currentDir.Substring(0, currentDir.IndexOf("bin"));
            //if the specific level is -1, we will run all the levels, otherwise we will run the specific level
            string[,] LevelFiles = new string[specificLevel == -1 ? 6 : 1, 3];

            if (specificLevel != -1)
            {
                LevelFiles[0, 0] = @$"{currentDir}level{level}\level{level}_{specificLevel}.in";
                LevelFiles[0, 1] = @$"{currentDir}level{level}\level{level}_{specificLevel}.out";
                LevelFiles[0, 2] = @$"{currentDir}level{level}\level{level}_{specificLevel}.debug";
            }
            else
            {
                LevelFiles[0, 0] = @$"{currentDir}level{level}\level{level}_example.in";
                LevelFiles[0, 1] = @$"{currentDir}level{level}\level{level}_example.out";
                LevelFiles[0, 2] = @$"{currentDir}level{level}\level{level}_example.debug";

                for (int i = 1; i < 6; i++)
                {
                    LevelFiles[i, 0] = @$"{currentDir}level{level}\level{level}_{i}.in";
                    LevelFiles[i, 1] = @$"{currentDir}level{level}\level{level}_{i}.out";
                    LevelFiles[i, 2] = @$"{currentDir}level{level}\level{level}_{i}.debug";
                }
            }

            Type type = Type.GetType("CCC.Level" + level) ?? throw new Exception("Couldn't find the file");
            ILevel levelInstance = (ILevel)Activator.CreateInstance(type);
            //file the instance with the data
            levelInstance.Level = level;
            levelInstance.Debug = debug;
            levelInstance.LevelFiles = LevelFiles;
            levelInstance.Run();
        }
    }
}
