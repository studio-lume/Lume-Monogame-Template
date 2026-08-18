using System.Collections.Generic;
using Project_Template.Source.Data.Enums;

namespace Project_Template.Source.Core.DrawPass {
    /// <summary>
    /// Object to carry the drawOrder data, since it gets passed through multiple files
    /// we want to retain the data, so we make a class out of it.
    /// </summary>
    public class DrawPassPass {
        public readonly Dictionary<DrawPassId, List<DrawInstance>> DrawOrder = [];
    }
}