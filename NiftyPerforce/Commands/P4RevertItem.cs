﻿// Copyright (C) 2006-2010 Jim Tilander. See COPYING for and README for more details.
using System.Windows.Forms;
using Aurora;
using EnvDTE;

namespace NiftyPerforce
{
    internal class P4RevertItem : ItemCommandBase
    {
        private readonly bool mOnlyUnchanged;

        public P4RevertItem(Plugin plugin, string canonicalName, bool onlyUnchanged)
            : base("RevertItem", canonicalName, plugin, true, true, onlyUnchanged ? PackageIds.NiftyRevertUnchanged : PackageIds.NiftyRevert)
        {
            mOnlyUnchanged = onlyUnchanged;
        }

        public override void OnExecute(SelectedItem item, string fileName)
        {
            if (!mOnlyUnchanged)
            {
                string message = "You are about to revert the file '" + fileName + "'. Do you want to do this?";
                if (MessageBox.Show(message, "Revert File?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return;
                }
            }

            P4Operations.RevertFile(fileName, mOnlyUnchanged);
        }
    }
}
