using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SEEA
{
    public class PickerViewOrganizationModel : INotifyPropertyChanged
    {

        public List<Organization> OrgList { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;


        public PickerViewOrganizationModel()
        {
            OrgList = GetOrgs().ToList();
        }



        protected void OnPropertyChanged([CallerMemberName] String name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Organization _selectedOrganization { set; get; }
        public Organization SelectedOrganization
        {

            get { return _selectedOrganization; }

            set
            {

                if (_selectedOrganization != value)
                {

                    _selectedOrganization = value;
                    MyOrganization = _selectedOrganization.OrgName;
                }
            }

        }

        public string _myOrg;
        public string MyOrganization
        {

            get { return _myOrg; }
            set
            {
                if (_myOrg != value)
                {
                    _myOrg = value;
                    OnPropertyChanged();
                }
            }

        }


        public List<Organization> GetOrgs()
        {

            var orgs = new List<Organization>() {

                new Organization(){ OrgName="Any",OrgId=1 },
                 new Organization(){ OrgName="Vaud",OrgId=2 },
                 new Organization() { OrgName="ZHAW", OrgId=3 },
                 new Organization() {OrgName="ETH", OrgId=4 },
                 new Organization() { OrgName="FHNW",OrgId= 5 },
                 new Organization() { OrgName="AKB",OrgId= 6 },
                 new Organization() { OrgName="Post",OrgId= 7 },
                 new Organization() {OrgName="ZKB",OrgId= 8 },
                 new Organization() { OrgName="BKB",OrgId= 9 }

            };

            return orgs;

        }

    }
}
