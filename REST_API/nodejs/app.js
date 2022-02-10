const express = require('express');
const app = express();
const port = 3000;

app.use(express.json());

//set up our server to listen on port 3000
var movies = [
    {title: "Star war", id:100, rating:10.0},
    {title: "Iron man", id:101, rating:20.0}
];

var nextId = 102;

//setting up endpoints
app.get('/movies', (request, response)=>{
    response.send(movies);
})

app.get('/movies/:id', (request, response)=>{
    for(var movie of movies){
        var targetId = request.params.id;
        if(movie.id == targetId){
            response.send(movie);
        }
    }

    response.send({status:400, error:`No movie found for ${request.params.id}`});
});

app.post('/movies/add', (request, response)=>{
    var data = request.body;
    var movie = {title: data.title, rating: data.rating, id:nextId};
    movies.push(movie);
    nextId++;

    response.send(movie);
})

app.listen(port, ()=>{
    console.log(`app listening on port ${port}`);
})