using System;
using Project_Template.Source.Data.Enums;

namespace Project_Template.Source.Actors.Test {
    public class BActor() : ActorBehaviour(DrawPassId.Test) {
        public override void Start() {
            Console.WriteLine("B Instaniated");
        }
    }
}