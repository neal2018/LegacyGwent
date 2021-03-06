using System;

namespace Cynthia.Card.Server
{
    public class GwentRoom
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public GwentServerGame CurrentGame { get; set; }

        //该房间的密匙
        public string Password { get; set; }
        public bool IsReady { get => Player2 != null && Player1 != null; }

        public bool IsPopup { get; set; } = false;

        public GwentRoom(Player player, string password)
        {
            Player1 = player;
            Password = password;
        }
        public bool AddPlayer(Player player)
        {
            if (Player1 == null)
            {
                Player1 = player;
                return true;
            }
            if (Player2 == null)
            {
                Player2 = player;
                return true;
            }
            return false;
        }
    }
}