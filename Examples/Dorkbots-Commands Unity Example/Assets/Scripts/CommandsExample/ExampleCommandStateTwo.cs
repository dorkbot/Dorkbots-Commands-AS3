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
    public class ExampleCommandStateTwo : CommandsState, ICommandsState
    {
        private MonoBehaviour monoBehaviourObject;

        public ExampleCommandStateTwo(MonoBehaviour monoBehaviourObject)
        {
            this.monoBehaviourObject = monoBehaviourObject;

            // Have to call the Parents Init() method
            Init();
        }

        protected override void SetupCommands()
        {
            // add a single command to the root commands
            rootCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 21"));

            // added parallel commands to the root
            ParallelCommands parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 22"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 23"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 24"));
            rootCommands.AddCommand(parallelCommands);

            parallelCommands = new ParallelCommands();

            // add serial commmands to parallel commands then add to root
            SerialCommands serialCommands = new SerialCommands();
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 25"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 26"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 27"));
            parallelCommands.AddCommand(serialCommands);

            // add serial commmands to parallel commands then add to root
            serialCommands = new SerialCommands();
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 28"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 29"));
            serialCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 30"));
            parallelCommands.AddCommand(serialCommands);

            // add serial commmands to parallel commands then add to root
            rootCommands.AddCommand(parallelCommands);

            serialCommands = new SerialCommands();

            // Add parallel commands to serial commands then add to root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 31"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 32"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 33"));
            serialCommands.AddCommand(parallelCommands);

            // Add parallel commands to serial commands then add to root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 34"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 35"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 36"));
            serialCommands.AddCommand(parallelCommands);

            // Add parallel commands to serial commands then add to root
            rootCommands.AddCommand(serialCommands);

            // add parallel commands to the root
            parallelCommands = new ParallelCommands();
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 37"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 38"));
            parallelCommands.AddCommand(new ExampleCommandMonoBehaviour().Init(monoBehaviourObject, null, "Command 39"));

            rootCommands.AddCommand(parallelCommands);

            // add the lighter commands that don't have the built in reference to a MonoBehaviour Object
            rootCommands.AddCommand(new ExampleCommand().Init(null, "Command 40"));
        }
    }
}