# LongRunningOperationDemo
Long Running Operations Reference Implementation

This is a reference implementation of a long running process using WCF and IIS.

Features:

- Doesn't allow to trigger the start more than once
- Supports multiple clients
- Any client can request start a process
- Clients support connect and disconnect, even when the client is a web application.
- Uses angular to indicate the state of the processing and the status of the long running process
