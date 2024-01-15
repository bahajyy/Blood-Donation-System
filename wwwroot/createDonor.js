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


function previewImage() {
    const preview = document.getElementById("image-preview");
    const fileInput = document.getElementById("photo");
    const file = fileInput.files[0];

    if (file) {
        const reader = new FileReader();
        reader.onload = function(e) {
            preview.innerHTML = `<img src="${e.target.result}" alt="Preview">`;
        };

        reader.readAsDataURL(file);
    } else {
        preview.innerHTML = ""; 
    }
}


function saveDonor() {
    const donorName = document.getElementById("donorName").value;
    const bloodType = document.getElementById("bloodType").value;
    const city = document.getElementById("city").value;
    const town = document.getElementById("town").value;
    const phone = document.getElementById("phone").value;
    const photo = document.getElementById("photo").files[0];

   
    const formData = new FormData();
    formData.append("donorName", donorName);
    formData.append("bloodType", bloodType);
    formData.append("city", city);
    formData.append("town", town);
    formData.append("phone", phone);
    formData.append("photo", photo);

    
    const apiUrl = "https://bloodproject.azurewebsites.net/api/Donors/create";


    
    fetch(apiUrl, {
        method: "POST",
        body: formData,
    })
    .then(response => response.json())
    .then(data => {
       
        console.log("Donor created successfully:", data);

        displayDonorInfo(donorName, bloodType, city, town, phone);
    })
    .catch(error => {
        console.error("Error creating donor:", error);
    });
}


function displayDonorInfo(donorName, bloodType, city, town, phone) {
    const donorTable = document.getElementById("donor-list");
    const newRow = donorTable.insertRow();

   
    const nameCell = newRow.insertCell(0);
    const bloodTypeCell = newRow.insertCell(1);
    const cityCell = newRow.insertCell(2);
    const townCell = newRow.insertCell(3);
    const phoneCell = newRow.insertCell(4);
    const actionsCell = newRow.insertCell(5);

    
    nameCell.textContent = donorName;
    bloodTypeCell.textContent = bloodType;
    cityCell.textContent = city;
    townCell.textContent = town;
    phoneCell.textContent = phone;

   
    actionsCell.innerHTML = `
        <button onclick="editDonor('${donorName}')">Edit</button>
        <button class="delete-btn" onclick="deleteDonor('${donorName}')">Delete</button>
    `;
}


function editDonor(donorName) {
    
    console.log("Edit donor:", donorName);
}


function deleteDonor(donorName) {
  
    console.log("Delete donor:", donorName);
}

