using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netzwerklabyrinth_V_Console
{
    public enum MessageTyp
    {
        OutputAndInput = 0,
        Input
    }
    class KommunikationManger
    {
        Messgae _Message;
        Answer answer;

        public KommunikationManger(InputTyp answerTyp)
        {
            this._Message = new Messgae();
            this.answer = new Answer(answerTyp);
        }
        public string messageManger(MessageTyp messageTyp, string message)
        {
            string input = "";
            if (messageTyp == MessageTyp.OutputAndInput)
            {
                _Message.MessageManger(message);

                if (answer.inputTyp == InputTyp.Manuel)
                    input = answer.ManuelInput();

                else if (answer.inputTyp == InputTyp.Automatisch)
                {
                    answer.LabyrinthUpdate(_Message.Labyrinth._Labyrinth, _Message.Labyrinth.startNode, _Message.Labyrinth.current);

                    input = answer.AutomatischInput();
                }
            }
            else if (messageTyp == MessageTyp.Input)
            {
                answer.answertyp = Answertyp.Move;
                input = answer.ManuelInput();
            }
            return input;
        }
    }
}
