const link = document.createElement('link');
link.rel = 'stylesheet';
link.href = 'https://fonts.googleapis.com/css?family=Roboto:400,700';
document.head.appendChild(link);

// Apply the font to an element
document.getElementById('items').style.fontFamily = 'Roboto';

const myFont = new FontFace('Pacifico', 'url(https://yourdomain.com/fonts/Pacifico.woff2)');
myFont.load().then(function(loadedFont) {
  document.fonts.add(loadedFont);
  document.getElementById('items').style.fontFamily = 'Pacifico';
});

fetch('/api/font-url')
  .then(res => res.json())
  .then(data => {
    const style = document.createElement('style');
    style.innerHTML = `
      @font-face {
        font-family: '${data.name}';
        src: url('${data.url}') format('woff2');
      }
    `;
    document.head.appendChild(style);
    document.getElementById('items').style.fontFamily = data.name;
  });

