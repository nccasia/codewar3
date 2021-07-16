const express = require('express')
const fs = require('fs');
const path = require('path');
const ytdl = require('ytdl-core');

const app = express()
const port = 3000
const baseUrl = `http://localhost:${port}`;

app.use(express.urlencoded());
app.use(express.json());

const folder = path.join(__dirname, 'musics');
app.use(express.static(folder))

app.all('*', function (req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "X-Requested-With");
    next();
});

app.get('/', (req, res) => {
    res.send('Hello World!')
})


app.post('/save-music', (req, res) => {
    try {
        console.log('rep', req.body);
        const { links } = req.body;

        if (!fs.existsSync(folder)) {
            fs.mkdirSync(folder);
        }

        for (let i = 0; i < links.length; i++) {
            const fileName = path.join(folder, `${i}.mp4`);
            ytdl(links[i]).pipe(fs.createWriteStream(fileName));
        }

        res.send('Success!')
    } catch (error) {
        console.error(error);
    }
})

app.get('/lists', (req, res) => {
    try {
        const fileNames = [];
        var files = fs.readdirSync(folder);
        files.forEach(file => {
            console.log(file);
            fileNames.push(baseUrl + '/' + file);
        });

        console.log('fileNames', fileNames);
        res.send(fileNames)
    } catch (error) {
        console.error(error);
    }
})

app.listen(port, () => {
    console.log(`Example app listening at ${baseUrl}`)
})