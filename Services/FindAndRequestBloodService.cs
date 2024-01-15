using BloodDonorSystem.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Collections.Generic;

namespace BloodDonorSystem.Services
{
    public class FindAndRequestBloodService : IFindAndRequestBloodService
    {
        private static Queue<BloodRequestModel> bloodRequestQueue = new Queue<BloodRequestModel>();
        private static List<BloodBankModel> bloodBanks = new List<BloodBankModel>
        {
            new BloodBankModel { Name = "BloodBank1", Units = 10, AvailableBloodTypes = new List<string> { "A+", "B+" } },
            new BloodBankModel { Name = "BloodBank2", Units = 5, AvailableBloodTypes = new List<string> { "A-", "O+" } },
        };

        public IActionResult RequestBlood(BloodRequestModel bloodRequest)
        {
            try
            {
                if (!IsValidBloodRequest(bloodRequest, out var validationError))
                {
                    return new BadRequestObjectResult(validationError);
                }

                var availableBlood = SearchAvailableBlood(bloodRequest.City, bloodRequest.Town, bloodRequest.BloodType);

                if (availableBlood > 0)
                {
                    SendEmail(bloodRequest.ContactEmail, $"Blood found! {availableBlood} units available.");
                    return new OkObjectResult($"Blood request fulfilled immediately. {bloodRequest.Units} units provided.");
                }
                else
                {
                    AddRequestToQueue(bloodRequest);

                    return new OkObjectResult("Blood request added to the queue. We will notify you once blood becomes available.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing blood request: {ex.Message}");
                return new StatusCodeResult(500);
            }
        }

        public IActionResult ProcessQueue()
        {
            while (bloodRequestQueue.Count > 0)
            {
                var bloodRequest = bloodRequestQueue.Dequeue();
                var fulfilled = SearchBloodBanks(bloodRequest);

                if (fulfilled)
                {
                    SendEmail(bloodRequest.ContactEmail, "Blood found in nearby blood bank!");
                    return new OkObjectResult("Blood request fulfilled from nearby blood bank.");
                }
            }

            return new OkObjectResult("No outstanding blood requests in the queue.");
        }

        private bool IsValidBloodRequest(BloodRequestModel bloodRequest, out string validationError)
        {
            validationError = "Validation failed!"; 
            return true; 
        }

        private int SearchAvailableBlood(string city, string town, string bloodType)
        {
            var random = new Random();
            return random.Next(0, 10);
        }

        private void AddRequestToQueue(BloodRequestModel bloodRequest)
        {
            bloodRequestQueue.Enqueue(bloodRequest);
            Console.WriteLine($"Blood request added to the queue: {bloodRequest.Units} units for {bloodRequest.City}, {bloodRequest.Town}, {bloodRequest.BloodType}");
        }

        private bool SearchBloodBanks(BloodRequestModel bloodRequest)
        {
            var requesterLocation = GetLocation(bloodRequest.City, bloodRequest.Town);

            foreach (var bloodBank in bloodBanks)
            {
                var distance = CalculateDistance(requesterLocation, new Location { Latitude = 0, Longitude = 0 });

                if (distance <= 50 && bloodBank.AvailableBloodTypes.Contains(bloodRequest.BloodType) && bloodBank.Units >= bloodRequest.Units)
                {
                    bloodBank.Units -= bloodRequest.Units;
                    return true;
                }
            }

            return false;
        }

        private Location GetLocation(string city, string town)
        {
            return new Location { Latitude = 0, Longitude = 0 };
        }

        private double CalculateDistance(Location location1, Location location2)
        {
            var latDiff = Math.Abs(location1.Latitude - location2.Latitude);
            var lonDiff = Math.Abs(location1.Longitude - location2.Longitude);
            return Math.Sqrt(Math.Pow(latDiff, 2) + Math.Pow(lonDiff, 2));
        }

        private void SendEmail(string toEmail, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Blood Donor System", "blooddonorsystem@example.com"));
                emailMessage.To.Add(new MailboxAddress("", toEmail));
                emailMessage.Subject = "Blood Donor System - Blood Found";

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.TextBody = message;

                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.example.com", 587, false);
                    client.Authenticate("your@email.com", "your-password");

                    client.Send(emailMessage);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }

    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
