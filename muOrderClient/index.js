window.onload = function () {
    const baseUrl = `http://localhost:3000/lists`;
    fetch(baseUrl).then(async (res) => {
        const data = await res.json();
        if (data) {
            console.log("data", data);
            var fileNames = data;
            var index = 0;

            var myAudio = document.getElementById('my-audio');
            myAudio.setAttribute('src', fileNames[index]);
            myAudio.play();

            // display progress
            myAudio.addEventListener('timeupdate', function () {
                console.log('timeupdate')
            });

            myAudio.addEventListener("ended", function () {
                console.log('ended')
                myAudio.removeAttribute('src');
                index = index + 1;
                myAudio.setAttribute('src', fileNames[index]);
                myAudio.load();
                myAudio.play();
            });
        }
    });
}