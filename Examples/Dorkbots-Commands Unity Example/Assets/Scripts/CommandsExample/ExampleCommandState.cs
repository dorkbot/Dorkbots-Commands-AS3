/*
* Author: Dayvid jones
* http://www.dayvid.com
* Copyright (c) Superhero Robot 2018
* http://www.superherorobot.com
* Manged by Dorkbots
* http://www.dorkbots.com/
* Version: 1
* 
* Licence Agreement
*
* You may distribute and modify this class freely, provided that you leave this header intact,
* and add appropriate headers to indicate your changes. Credit is appreciated in applications
* that use this code, but is not required.
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/
using Dorkbots.DorkbotsCommands;
using UnityEngine;

namespace CommandsExample
{
    public class ExampleCommandState : CommandsState, ICommandsState
    {
        private MonoBehaviour monoBehaviourObject;

        public ExampleCommandState(MonoBehaviour monoBehaviourObject)
        {
            this.monoBehaviourObject = monoBehaviourObject;

            // Have to call the Parents Init() method
            Init();
        }

        protected override void SetupCommands()
        {
            // add a single command to the root commands
            rootCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 1"));

            // added parallel commands to the root
            ParallelCommands parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 2"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 3"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 4"));
            rootCommands.AddCommand(parallelCommands);

            parallelCommands = new ParallelCommands();

            // add serial commmands to parallel commands then add to root
            SerialCommands serialCommands = new SerialCommands();
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 5"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 6"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 7"));
            parallelCommands.AddCommand(serialCommands);

            // add serial commmands to parallel commands then add to root
            serialCommands = new SerialCommands();
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 8"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 9"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 10"));
            parallelCommands.AddCommand(serialCommands);

            // add serial commmands to parallel commands then add to root
            rootCommands.AddCommand(parallelCommands);

            serialCommands = new SerialCommands();

            // Add parallel commands to serial commands then add to root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 11"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 12"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 13"));
            serialCommands.AddCommand(parallelCommands);

            // Add parallel commands to serial commands then add to root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 14"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 15"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 16"));
            serialCommands.AddCommand(parallelCommands);

            // Add parallel commands to serial commands then add to root
            rootCommands.AddCommand(serialCommands);

            // add parallel commands to the root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 17"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 18"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 19"));

            rootCommands.AddCommand(parallelCommands);

            // add the lighet commands that don't have the built in reference to a MonoBehaviour Object
            rootCommands.AddCommand(new ExampleCommand().Init(null, "Command 20"));
        }
    }
}