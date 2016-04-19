# TechXplorers.ArgumentValidation
Fluent method argument validation utility

Sample Usage:

void Foo(int? id, string name)

{
    
    Check
    
	    .That(() => id).When(It.HasValue).IsGreaterThan(0)
	    
	    .AndThat(() => name).IsNotWhitespace();
	    

	//....
}

/*
If any of the arguments do not satisfy the condition, will throw TechXplorers.ArgumentValidation.ArgumentNotValidException with the details
*/
 
