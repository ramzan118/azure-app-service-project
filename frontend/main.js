fetch('https://expo-backend-app.azurewebsites.net/api/items')
  .then(res => res.json())
  .then(data => {
    const ul = document.getElementById('items');
    data.forEach(item => {
      const li = document.createElement('li');
      li.innerText = item.name;
      ul.appendChild(li);
    });
  })
  .catch(console.error);

