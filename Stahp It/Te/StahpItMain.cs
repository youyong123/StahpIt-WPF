﻿/*
* Copyright (c) 2016 Jesse Nicholson.
*
* This file is part of Stahp It.
*
* Stahp It is free software: you can redistribute it and/or
* modify it under the terms of the GNU General Public License as published
* by the Free Software Foundation, either version 3 of the License, or (at
* your option) any later version.
*
* In addition, as a special exception, the copyright holders give
* permission to link the code of portions of this program with the OpenSSL
* library.
*
* You must obey the GNU General Public License in all respects for all of
* the code used other than OpenSSL. If you modify file(s) with this
* exception, you may extend this exception to your version of the file(s),
* but you are not obligated to do so. If you do not wish to do so, delete
* this exception statement from your version. If you delete this exception
* statement from all source files in the program, then also delete it
* here.
*
* Stahp It is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General
* Public License for more details.
*
* You should have received a copy of the GNU General Public License along
* with Stahp It. If not, see <http://www.gnu.org/licenses/>.
*/

using NLog;
using System;
using Microsoft.VisualBasic.ApplicationServices;

namespace Te.StahpIt
{
    public class SingleAppInstanceManager : WindowsFormsApplicationBase
    {
        private StahpIt m_app;

        public SingleAppInstanceManager()
        {
            IsSingleInstance = true;
        }

        protected override bool OnStartup(StartupEventArgs eventArgs)
        {   
            m_app = new StahpIt();
            m_app.InitializeComponent();
            m_app.Run();
            return false;
        }

        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            base.OnStartupNextInstance(eventArgs);
            m_app.BringAppToFocus();
        }
    }

    public static class StahpItMain
    {
        public static readonly Logger Logger = LogManager.GetLogger("StahpIt");

        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                SingleAppInstanceManager appManager = new SingleAppInstanceManager();
                appManager.Run(args);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}