using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Ui.Wp8.Infrastructure
{
    public class TileUpdater
    {
        private const Uri RegularIcon = new Uri("SteeringWheel.png", UriKind.Relative);
        private const Uri WarningIcon = new Uri("SteeringWheel.png", UriKind.Relative);

        public void SetGood()
        {
            Update(Colors.Green, "Good Driving!", RegularIcon, RegularIcon);
        }

        public void SetSpeeding()
        {
            Update(Colors.Red, "Speeding!", WarningIcon, WarningIcon);
        }

        public void SetHighAcceleration()
        {
            Update(Colors.Green, "High Acceleration!", WarningIcon, WarningIcon);
        }

        public void SetAbruptBreaking()
        {
            Update(Colors.Orange, "Abrupt Breaking!", WarningIcon, WarningIcon);
        }

        private void Update(Color backgroundColor, string message, Uri icon, Uri smallIcon)
        {
            IconicTileData TileData = new IconicTileData()
            {
                WideContent1 = message,
                BackgroundColor = backgroundColor,
                IconImage = icon,
                SmallIconImage = smallIcon
            };
            ShellTile.ActiveTiles.First().Update(TileData);
        }
    }
}