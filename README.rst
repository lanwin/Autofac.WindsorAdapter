Autofac.WindsorAdapter
==============
This is an adapter which changes or creates a Castle Windsor instance so it redirects ALL registration and resolve requests to Autofac. 

I created this project primary to use Autofac in conjunection with Rhino.ServiceBus (Rhine.ESB).

Home: http://github.com/lanwin/Autofac.WindsorAdapter

Current state
==============
For now this is a toy project where i try to find out if this could work.

Its able to send and receive messages via Rhino.ESB and inject Autofac dependencies.
