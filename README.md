# Long Running Operations (Reference Implementation)

This is a reference implementation of a long running process using WCF, 
a console application as a long running process and IIS as a client.

Features:
- Doesn't allow to trigger the start more than once
- Supports multiple clients
- Any client can request start a process
- Clients support connect and disconnect, even when the client is a web application.
- Uses angular to indicate the state of the processing and the status of the long running process


Languages and technologies used:
- C#, WCF
- JavaScript, Angular
- BootStrap, CSS

-------------------------
The MIT License

Copyright (c) 2016 Roberto Carrillo, http://www.robertocarrillo.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.