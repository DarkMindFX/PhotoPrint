﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoPrint.Interfaces
{
    public interface IInitParams
    {
        public Dictionary<string, string> Parameters
        {
            get;
            set;
        }
    }

    public interface IInitializable
    {
        void Init(IInitParams initParams);

        IInitParams CreateInitParams();
    }
}
