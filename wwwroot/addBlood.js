
const branchName = prompt("Enter your branch name:");
const branchPassword = prompt("Enter your branch password:");

function addBlood() {
    const bloodType = document.getElementById("bloodType").value;
    const donorName = document.getElementById("donorName").value;
    const units = document.getElementById("units").value;
    const donationDate = document.getElementById("donationDate").value;

    
    if (branchName && branchPassword) {
        const apiUrl = "https://bloodproject.azurewebsites.net/api/BloodDonations";
        const requestData = {
            branchName: branchName,
            branchPassword: branchPassword,
            bloodType: bloodType,
            donorName: donorName,
            units: units,
            donationDate: donationDate
        };

        fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(requestData)
        })
        .then(response => response.json())
        .then(data => {
            console.log("Blood added successfully:", data);
            alert("Blood added successfully!");
        })
        .catch(error => {
            console.error("Error adding blood:", error);
            alert("Error adding blood. Please try again.");
        });
    } else {
        alert("Invalid credentials. Please enter valid branch name and password.");
    }
}
