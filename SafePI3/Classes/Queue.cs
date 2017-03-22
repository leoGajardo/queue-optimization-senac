using SafePI3.Interfaces;
using SafePI3.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePI3.Classes
{
    public class Queue : IQueue, IDisposable
    {
        private List<Client> _Clients;
        public List<Client> Clients
        {
           get
            {
                return _Clients;
            }
            set
            {
                if (value != _Clients) { 
                    _Clients = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private string _Name;
        public string Name
        {
           get
            {
                return _Name;
            }
            set
            {
                if (value != _Name) { 
                    _Name = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private int _OperatorsQuantity;
        public int OperatorsQuantity
        {
           get
            {
                return _OperatorsQuantity;
            }
            set
            {
                if (value != _OperatorsQuantity) { 
                    _OperatorsQuantity = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private int _QueuesQuantity;
        public int QueuesQuantity
        {
           get
            {
                return _QueuesQuantity;
            }
            set
            {
                if (value != _QueuesQuantity) { 
                    _QueuesQuantity = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private int _ServiceDesksQuantity;
        public int ServiceDesksQuantity
        {
           get
            {
                return _ServiceDesksQuantity;
            }
            set
            {
                if (value != _ServiceDesksQuantity) { 
                    _ServiceDesksQuantity = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private string _Label;
        public string Label
        {
            get
            {
                return _Label;
            }
            set
            {
                if (value != _Label) { 
                    _Label = value;
                    UpdateQueueUserControl();
                }
            }
        }

        private QueueUC _QueueUserControl;
        public QueueUC QueueUserControl
        {
            get
            {
                if (_QueueUserControl == null)
                {
                    _QueueUserControl = new QueueUC(this);
                }
                return _QueueUserControl;
            }
            set
            {
                _QueueUserControl = value;
            }
        }

        public Queue(string label , string name , int operatorsQuantity , int queuesQuantity , int serviceDesksQuantity , List<Client> clients)
        {
            _Label = label;
            _Name = name;
            _OperatorsQuantity = operatorsQuantity;
            _QueuesQuantity = queuesQuantity;
            _ServiceDesksQuantity = serviceDesksQuantity;
            _Clients = clients;
        }

        public void Dispose()
        {
            QueueUserControl.Dispose();
        }

        private void UpdateQueueUserControl()
        {
            QueueUserControl.UpdateQueue();
        }
    }
}
