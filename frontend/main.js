// 1) Load Google Font
const link = document.createElement('link');
link.rel = 'stylesheet';
link.href = 'https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap';
document.head.appendChild(link);

// 2) Fetch and render exhibitions
async function loadExhibitions() {
  try {
    const res = await fetch('data/exhibitions.json');
    const exhibitions = await res.json();

    const container = document.getElementById('items');
    container.innerHTML = ''; // clear loading text

    exhibitions.forEach(ex => {
      const card = document.createElement('div');
      card.className = 'card';
      card.innerHTML = `
        <img src="${ex.image}" alt="${ex.title}" />
        <div class="info">
          <h3>${ex.title}</h3>
          <p><strong>Date:</strong> ${ex.date}</p>
          <p><strong>Location:</strong> ${ex.location}</p>
          <button onclick="book(${ex.id})">Book Now</button>
        </div>
      `;
      container.appendChild(card);
    });
  } catch (err) {
    document.getElementById('items').textContent = 'Failed to load exhibitions.';
    console.error(err);
  }
}

// 3) Mock “Book Now” action
function book(id) {
  alert(`You clicked Book for exhibition #${id}. Integrate your booking API here.`);
}

// 4) Initial placeholder & kick off load
document.getElementById('items').textContent = 'Loading exhibitions…';
loadExhibitions();

