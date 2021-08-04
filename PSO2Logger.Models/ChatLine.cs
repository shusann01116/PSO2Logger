using System;

namespace PSO2Logger.Models {
    public class ChatLine : ModelBase {
        private DateTime timeStamp;
        public DateTime TimeStamp {
            get { return timeStamp; }
            set { SetProperty(ref timeStamp, value); }
        }

        private int chatNum;
        public int ChatNum {
            get { return chatNum; }
            set { SetProperty(ref chatNum, value); }
        }

        private ChatType chatType;
        public ChatType ChatType {
            get { return chatType; }
            set { SetProperty(ref chatType, value); }
        }

        private int playerID;
        public int PlayerID {
            get { return playerID; }
            set { SetProperty(ref playerID, value); }
        }

        private string playerIDName;
        public string PlayerIDName {
            get { return playerIDName; }
            set { SetProperty(ref playerIDName, value); }
        }

        private string chatBody;
        public string ChatBody {
            get { return chatBody; }
            set { SetProperty(ref chatBody, value); }
        }
    }
}
