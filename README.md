libAPNs
=============

This is a C# library for interfacing with Apple's Push Notification Service.

Usage
-------

Load your certificate:

    var certificatePath = "PATH_TO_CERTIFICATE"; // <- change this
    var certificatePassword = "PASSWORD"; // <- change this
    var clientCertificate = new X509Certificate2(certificatePath, certificatePassword);

Create a new payload:

    var payload = new Payload(new PayloadAlertMessage("Test Message"), 1, "beep.wav");

Create a device token object with the device id you want to send to:

    var deviceToken = new DeviceToken("fe58fc8f527c363d1b775dca133e04bff24dc5032d08836992395cc56bfa62ef");

Create a new notification, both simple and enhanced notifications are supported:

    var simpleNotification = new SimpleNotification(deviceToken, payload);
    // an enhanced notification with an identifier set to 1234 and an expiration set to 1 day.
    var enhancedNotification = new EnhancedNotification(deviceToken, payload, 1234, new TimeSpan(1, 0, 0, 0)); 

Create a new APNS object with the certificate:

    var pushService = new APNS(true, clientCertificate); // a push service that will connect to the sandbox apns

Enhanced notifications will return a response:

    var response = pushService.SendEnhancedNotification(enhancedNotification);
    if (response != null) {
        // an error occurred
        Console.WriteLine(response.StatusCode);
        Console.WriteLine(response.Identifier);
    }

Simple notifications don't return anything:

    pushService.SendSimpleNotification(simpleNotification);

That's It!

Full Sample
-------

    class Program
    {
      
        static void Main(string[] args)
        {
            //load certificate
            var certificatePath = "apple_push_notification_certificate.p12";
            var certificatePassword = "";
            var clientCertificate = new X509Certificate2(certificatePath, certificatePassword);

            var payload = new Payload(new PayloadAlertMessage("Test Message"), 1, "beep.wav");
            var deviceToken = new DeviceToken("fe58fc8f527c363d1b775dca133e04bff24dc5032d08836992395cc56bfa62ef");

            var notification = new EnhancedNotification(deviceToken, payload, 1234, new TimeSpan(1, 0, 0, 0));

            var s = new APNS(true, clientCertificate);
            var response = s.SendEnhancedNotification(notification);
            if (response != null)
            {
                Console.WriteLine("Received Error!");
                Console.WriteLine("Identifier: {0}", response.Identifier);
                Console.WriteLine(response.StatusCode);
            }


            Console.Write("Message sent!");
            

            Console.Read();
        }
    }
