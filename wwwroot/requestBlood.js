const cities = ["İstanbul", "Ankara", "İzmir"];
const towns = {
    "İstanbul": ["Beşiktaş", "Kadıköy", "Şişli"],
    "Ankara": ["Çankaya", "Kızılay", "Keçiören"],
    "İzmir": ["Konak", "Buca", "Karşıyaka"]
};


document.addEventListener("DOMContentLoaded", function() {
    fillCityDropdown();
});

function fillCityDropdown() {
    const cityDropdown = document.getElementById("city");
    cities.forEach(city => {
        const option = document.createElement("option");
        option.value = city;
        option.text = city;
        cityDropdown.add(option);
    });
    fillTownDropdown(cities[0]);
}
document.getElementById("city").addEventListener("change", function() {
    const selectedCity = this.value;
    fillTownDropdown(selectedCity);
});

function fillTownDropdown(city) {
    const townDropdown = document.getElementById("town");
    townDropdown.innerHTML = ""; 

    towns[city].forEach(town => {
        const option = document.createElement("option");
        option.value = town;
        option.text = town;
        townDropdown.add(option);
    });
}

function requestBlood() {
    const requesterHospital = document.getElementById("requesterHospital").value;
    const city = document.getElementById("city").value;
    const town = document.getElementById("town").value;
    const bloodType = document.getElementById("bloodType").value;
    const units = document.getElementById("units").value;
    const contactEmail = document.getElementById("contactEmail").value;
    const searchDuration = document.getElementById("searchDuration").value;
    const reason = document.getElementById("reason").value;
    const apiUrl = "https://bloodproject.azurewebsites.net/api/BloodRequest/request";

    const requestData = {
        requesterHospital: requesterHospital,
        city: city,
        town: town,
        bloodType: bloodType,
        units: units,
        contactEmail: contactEmail,
        searchDuration: searchDuration,
        reason: reason
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
        console.log("Blood request submitted successfully:", data);
        alert("Blood request submitted successfully!");
    })
    .catch(error => {
        console.error("Error submitting blood request:", error);
        alert("Error submitting blood request. Please try again.");
    });
}
