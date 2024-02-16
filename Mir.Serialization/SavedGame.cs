using System.IO;
using Mir.Serialization.Utility;

namespace Mir.Serialization
{
    public class SavedGame
    {
        private const string FILE_NAME = "mir.savedgame";

        private readonly Header header;
        private readonly Data data;

        public SavedGame(Header header, Data data)
        {
            this.header = header;
            this.data = data;
        }

        public static SavedGame Create(int cameraYAxisOffset, float playerX, float playerY)
        {
            return Create(cameraYAxisOffset, playerX, playerY, false);
        }

        public static SavedGame Create(int cameraYAxisOffset, float playerX, float playerY, bool serialize)
        {
            Header header = new Header(Header.MAGIC_NUMBER);
            Data data = new Data(cameraYAxisOffset, playerX, playerY);
            SavedGame savedGame = new SavedGame(header, data);

            if (serialize)
            {
                savedGame.Serialize();
            }

            return savedGame;
        }

        public static SavedGame CreateFileAsDefault()
        {
            return CreateFileAsDefault(false);
        }

        public static SavedGame CreateFileAsDefault(bool serialize)
        {
            // Constant variables
            const int defaultCameraYAxisOffset = 0;
            const float defaultPlayerX = 0f;
            const float defaultPlayerY = 0f;

            Header header = new Header(Header.MAGIC_NUMBER);
            Data data = new Data(defaultCameraYAxisOffset, defaultPlayerX, defaultPlayerY);
            SavedGame savedGame = new SavedGame(header, data);

            if (serialize)
            {
                savedGame.Serialize();
            }

            return savedGame;
        }

        public static SavedGame Deserialize()
        {
            // Open stream
            FileStream fs = File.OpenRead(FILE_NAME);
            DataInputStream dataInput = new DataInputStream(fs);

            // Read header
            int magicNumber = dataInput.ReadInt();

            // Read data
            int cameraYAxisOffset = dataInput.ReadInt();
            float playerX = dataInput.ReadFloat();
            float playerY = dataInput.ReadFloat();

            // Create object
            Header header = new Header(magicNumber);
            Data data = new Data(cameraYAxisOffset, playerX, playerY);
            SavedGame savedGame = new SavedGame(header, data);

            // Return object
            return savedGame;
        }

        public void Serialize()
        {
            // Open stream
            FileStream fs = File.OpenWrite(FILE_NAME);
            DataOutputStream dataOutput = new DataOutputStream(fs);

            // Write header
            dataOutput.WriteInt(header.MagicNumber);

            // Write data
            dataOutput.WriteInt(data.CameraYAxisOffset);
            dataOutput.WriteFloat(data.PlayerX);
            dataOutput.WriteFloat(data.PlayerY);

            // Close stream
            fs.Close();
        }

        public Header GetHeader() { return header; }
        public Data GetData() { return data; }

        public struct Header
        {
            // Constan variables
            public const int MAGIC_NUMBER = 0x0052494D;

            // Private variables
            private readonly int magicNumber;

            public Header(int magicNumber)
            {
                if (magicNumber != MAGIC_NUMBER)
                {
                    throw new System.Exception("Magic number was different: " + magicNumber + "(Expected: " + MAGIC_NUMBER + ")");
                }

                this.magicNumber = magicNumber;
            }

            public int MagicNumber
            {
                get
                {
                    return magicNumber;
                }
            }
        }

        public struct Data
        {
            // Private variables
            private readonly int cameraYAxisOffset;
            private readonly float playerX;
            private readonly float playerY;

            public Data(int cameraYAxisOffset, float playerX, float playerY)
            {
                this.cameraYAxisOffset = cameraYAxisOffset;
                this.playerX = playerX;
                this.playerY = playerY;
            }

            public int CameraYAxisOffset
            {
                get
                {
                    return this.cameraYAxisOffset;
                }
            }

            public float PlayerX
            {
                get
                {
                    return this.playerX;
                }
            }

            public float PlayerY
            {
                get
                {
                    return this.playerY;
                }
            }
        }
    }
}
