using UnityEngine;

namespace Nidavellir.EventArgs
{
    public class PlayerTypeEventArgs : System.EventArgs
    {
        public PlayerTypeEventArgs(PlayerType affectedPlayerType, Vector3 respawnPoint)
        {
            this.AffectedPlayerType = affectedPlayerType;
            this.RespawnPoint = respawnPoint;
        }

        public Vector3 RespawnPoint { get; }
        public PlayerType AffectedPlayerType { get; }
    }
}