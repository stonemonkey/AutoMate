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
        private readonly Uri RegularIcon = new Uri("SteeringWheel.png", UriKind.Relative);
        private readonly Uri WarningIcon = new Uri("SteeringWheel.png", UriKind.Relative);

        public void SetGood()
        {
            Update(Colors.Green, "Good Driving!", RegularIcon);
        }

        public void SetSpeeding()
        {
            Update(Colors.Red, "Speeding!", WarningIcon);
        }

        public void SetHighAcceleration()
        {
            Update(Colors.Orange, "Eratic Acceleration!", WarningIcon);
        }
        
        private void Update(Color backgroundColor, string message, Uri icon)
        {
            IconicTileData TileData = new IconicTileData()
            {
                WideContent1 = message,
                BackgroundColor = backgroundColor,
                IconImage = icon,
                SmallIconImage = icon
            };
            ShellTile.ActiveTiles.First().Update(TileData);
        }
    }
}