using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PROJECT_PILLENDOOS
{
    public class AlarmProcessor
    {
        private Action<byte> _sendSerialData;

        public AlarmProcessor(Action<byte> sendSerialData)
        {
            _sendSerialData = sendSerialData;
        }

        public void Process(ComboBox hourBox, ComboBox minuteBox, bool soundEnabled, byte data)
        {
           
            if (hourBox?.SelectedItem != null && minuteBox?.SelectedItem != null &&
                hourBox.SelectedItem.ToString() != "None" && minuteBox.SelectedItem.ToString() != "None")
            {
                int selectedHour = Convert.ToInt32(hourBox.SelectedItem);
                int selectedMinute = Convert.ToInt32(minuteBox.SelectedItem);
                DateTime currentTime = DateTime.Now;

              
                if (currentTime.Hour == selectedHour && currentTime.Minute == selectedMinute)
                {
                    _sendSerialData(data);

                 
                    if (soundEnabled)
                    {
                        _sendSerialData(5);
                    }
                }
            }
        }
    }
}
