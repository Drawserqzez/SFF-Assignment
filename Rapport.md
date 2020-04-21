# SFF-Assignment -- Algot Holton

## Git repo
> https://github.com/Drawserqzez/SFF-Assignment

--- 
## Documentation of endpoints

### MovieController - api/v1/Movies

| Route | Verb | Action |
| ----- | ---- | ------ |
|/|GET| Returns all movies as an array|
|/|POST|Posts a movie from json to the database|
|/id/{id}|Get|Returns movie with id = {id}|
|/title|GET|Returns movies with titles equal to a url parameter|
|/delete/movie|DELETE|Removes a movie from the database. Used to control amount of movies available. Returns all remaining movies.|
|/borrow|PUT|Changes a movie to borrowed to a certain studio|
|/return|PUT|Changes a movie from borrowed and removes the related studio|
|/add-trivia|POST|Adds a trivia connected to a movie to the database|
|/trivias|GET|Returns all trivia objects that match the url-parameter|
|/delete/trivia|DELETE|Removes a certain trivia object, returns remaining trivias.|
|/grades|POST|Creates a grade-object connected to a movie and a studio.|
|/grades|GET|Returns all grades connected to a provided movie|
|/label/{id}.{format}|GET|Returns a MovieLabel for the movie with id = {id} and {format} = .xml for a xml-result)|

### StudioController - api/v1/Studios

| Route | Verb | Action |
| ----- | ---- | ------ |
|/|GET|Returns all studios|
|/|POST|Posts a studio to the database.
|/id/{id}|GET|Returns studio with the id = {id}
|/delete|DELETE|Removes a studio from the database
|/borrowed/{id}|GET|Returns all movies that the studio with id = {id} has borrowed
|/edit|PUT|Edits the specified studio

## 