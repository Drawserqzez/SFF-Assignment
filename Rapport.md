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
|/id/{id}|GET|Returns movie with id = {id}|
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

### Are these endpoints fitting?

I'd say so, yes. This is because each endpoint clears up what it does through the route-names, since they're self-evident. 
This makes the API easier to interact with, in my opinion. 
I also think that my decision to use actual instances for each and every film made it easier to work with, since the amount of movies available can very easily be adjusted by adding or removing instances. 
The only endpoint that I'm not that happy with is the one made for returning labels in XML - since it needs a fileformat as a url parameter, which I assume could've been solved in a better way via a custom middleware.

---

## Modeling of the API

I've made the API to work with two controllers - one for movies and one for studios. 
This was because I felt there was no reason to have more controllers than that, since all actions that were demanded were either connected to movies or studios, mostly movies.
To handle data I also used a system with repositories that interacted with my database, instead of having the controllers do that themselves to increase encapsulation.

One of my endpoints returns XML in accordance to one of the demands. This was done with the help of a [tutorial](https://andrewlock.net/formatting-response-data-as-xml-or-json-based-on-the-url-in-asp-net-core/) because I didn't feel comfortable with writing my own middleware, since it feels like we haven't gone through that much. 
In the end I wish that I had written my own middleware anyway, because I don't like the fact that I need a file-ending in the url with this solution. 
Therefore I don't think this was the best available solution. but in the end I also don't think that my solution is bad. 

---

## Using LINQ 

In my repositories I've used LINQ fairly often, both the extensions and the actual query language. 

An examples from [MovieRepository.cs](src/SFF.Domain/Models/MovieRepository.cs): 

This snippet selects a movie with a LINQ query and then ensures that the result is either null or the first movie in the collection via a LINQ extension method.
```csharp
    var movieToReturn = (
        from m in _context.Movies
        where m.Title == movieTitle
        && m.Borrowed == true 
        && m.Borrower.ID == studioID
        select m
    ).FirstOrDefault();
``` 

This snippet uses a lambda-expression to do a similar thing to the query above.
```csharp
var movieToBorrow = _context.Movies.FirstOrDefault(
    m => m.Title == movieTitle && !m.Borrowed
);
```
---

## Models 

I've used 5 different models to handle my data. 
- [Movie](src/SFF.Domain/Models/Movie.cs)
- [Studio](src/SFF.Domain/Models/Studio.cs)
- [Grade](src/SFF.Domain/Models/Grade.cs)
- [Trivia](src/SFF.Domain/Models/Trivia.cs)
- [MovieLabel](src/SFF.Domain/Models/MovieLabel.cs)

These models are fairly simple, studio being the simplest since it's without references to any other objects.
Movie is also fairly simple, but it contains a reference to the studio that could have borrowed it. This is null if the movie isn't borrowed. Trivia contains a reference to a movie, and Grade contains references both to a movie and the studio that made the rating. MovieLabel is exclusively used to deliver data for the XML endpoint that required very specific data. MovieLabel isn't stored in the database.

The database has been populated via migrations - which I've had troubles with due to limitations with the Sqlite functionality. The migrations tool cannot alter the tables in the database, and hence I've had to delete and recreate the migration for every change instead of stacking migrations for each change of the code.

---

## Alternative solutions 

During the code review I noticed that some of my group-members had separate controllers for all the different models. 
I had also considered this approach, but I felt like it was redundant, since the operations involving the other models were connected to the movies. 
The upside of this approach, I think, would be that the code is inherently more readable, since the different operations are more split up, although I don't think that this is a very significant gain compared to the extra trouble it causes.

Another alternative solution would be to have all data operations in the controllers. This would've simplified the code, but would also hurt it due to the decreased amount of encapsulation. Controllers should be in charge of kinda pulling the strings of your different systems and let them handle the implementation themselves. This is important because it makes it easier to change the implementation without having to change the way the data is presented.

Yet another alternative that I considered using was including a variable for the amount of movies available to borrow in the Movie class. 
I decided against this in the end, because I felt it was easier to work with a separate instance for each film, since the movies then could keep track of if they're borrowed or not, as well as who has borrowed them, instead of that responsibility falling onto the Studio objects.