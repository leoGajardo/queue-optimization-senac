using SafePI3.Interfaces;
using SafePI3.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafePI3.Classes
{
    public class Pipe : IPipe, IDisposable
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
                    UpdatePipeUserControl();
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
                    UpdatePipeUserControl();
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
                    UpdatePipeUserControl();
                }
            }
        }

        private int _PipesQuantity;
        public int PipesQuantity
        {
           get
            {
                return _PipesQuantity;
            }
            set
            {
                if (value != _PipesQuantity) { 
                    _PipesQuantity = value;
                    UpdatePipeUserControl();
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
                    UpdatePipeUserControl();
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
                    UpdatePipeUserControl();
                }
            }
        }

        private PipeUC _PipeUserControl;
        public PipeUC PipeUserControl
        {
            get
            {
                if (_PipeUserControl == null)
                {
                    _PipeUserControl = new PipeUC(this);
                }
                return _PipeUserControl;
            }
            set
            {
                _PipeUserControl = value;
            }
        }

        public Pipe(string label , string name , int operatorsQuantity , int pipesQuantity , int serviceDesksQuantity , List<Client> clients)
        {
            _Label = label;
            _Name = name;
            _OperatorsQuantity = operatorsQuantity;
            _PipesQuantity = pipesQuantity;
            _ServiceDesksQuantity = serviceDesksQuantity;
            _Clients = clients;
        }

        public void Dispose()
        {
            PipeUserControl.Dispose();
        }

        private void UpdatePipeUserControl()
        {
            PipeUserControl.UpdatePipe();
        }
    }
}
