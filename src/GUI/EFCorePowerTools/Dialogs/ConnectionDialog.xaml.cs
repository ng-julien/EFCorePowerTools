﻿using System;
using System.Globalization;
using System.Windows.Controls;
using EFCorePowerTools.Common.Models;
using EFCorePowerTools.Contracts.ViewModels;
using EFCorePowerTools.Contracts.Views;
using RevEng.Common;

namespace EFCorePowerTools.Dialogs
{
    public partial class ConnectionDialog : IPickConnectionDialog
    {
        private readonly Func<DatabaseConnectionModel> getDialogResult;

        public ConnectionDialog(IPickConnectionViewModel viewModel)
        {
            DataContext = viewModel;
            viewModel.CloseRequested += (sender, args) =>
            {
                DialogResult = args.DialogResult;
                Close();
            };
            getDialogResult = () => viewModel.DatabaseConnection;

            InitializeComponent();
            cmdDatabaseType.SelectedIndex = 0;
        }

        (bool ClosedByOK, DatabaseConnectionModel Payload) IDialog<DatabaseConnectionModel>.ShowAndAwaitUserResponse(bool modal)
        {
            bool closedByOkay;

            if (modal)
            {
                closedByOkay = ShowModal() == true;
            }
            else
            {
                closedByOkay = ShowDialog() == true;
            }

            return (closedByOkay, getDialogResult());
        }

        private void CmdDatabaseType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            txtSample.Text = string.Empty;

            var item = cmdDatabaseType.SelectedItem as ComboBoxItem;

            if (item == null)
            {
                return;
            }

            var dbType = (DatabaseType)int.Parse(item.Tag.ToString(), CultureInfo.InvariantCulture);

            switch (dbType)
            {
                case DatabaseType.Undefined:
                case DatabaseType.SQLite:
                case DatabaseType.Npgsql:
                case DatabaseType.Oracle:
                case DatabaseType.SQLServerDacpac:
                    break;

                case DatabaseType.Mysql:
                    txtSample.Text = "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;";
                    break;
                case DatabaseType.SQLServer:
                    txtSample.Text = "Server=myserver.database.windows.net;Authentication=Active Directory Default;Database=mydatabase;User Id=user@domain.com";
                    break;
                case DatabaseType.Firebird:
                    txtSample.Text = "database=localhost:demo.fdb;user=sysdba;password=masterkey";
                    break;
                default:
                    break;
            }
        }
    }
}
