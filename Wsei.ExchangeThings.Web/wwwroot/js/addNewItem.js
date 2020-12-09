(function () {
    const alertElement = document.getElementById("success-alert");
    const errorAlertElement = document.getElementById("error-alert");
    const list = document.getElementById("list");
    const msg = document.getElementById("msg");
    const form = document.forms[0];
    const addNewItem = async () => {
        // 1. read data from the form
        const requestData = JSON.stringify({ Name: document.getElementById("Name").value, Description: document.getElementById("Description").value, IsVisible: document.getElementById("IsVisible").checked });
        // 2. call the application server using fetch method
        const url = "/api/item";
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: requestData
        });
        const responseJson = await response.json();
        if (responseJson.success) {
            alertElement.style.display = "";
            var HTML = "<table border=1 width=100%><tr><th>Name</th><th>Description</th><th>Visibility</th></tr>";

            responseJson.data.forEach(element => {
                HTML += "<tr><td align=center>" + element.name + "</td>";
                HTML += "<td align=center>" + element.description + "</td>";
                HTML += "<td align=center>" + element.isVisible + "</td></tr>";
            });

            HTML += "</table>";

            list.innerHTML = HTML;
            list.style.display =  "";
        } else {
            errorAlertElement.style.display = "";
            msg.innerText = responseJson.message;
        }
    };
    window.addEventListener("load", () => {
        form.addEventListener("submit", event => {
            event.preventDefault();
            alertElement.style.display = "none";
            errorAlertElement.style.display = "none";
            list.style.display = "none";
            addNewItem().then(() => console.log("added successfully"));
        });
    });
})();