
// type is an wrapper for a primitive type
type CustomerId = CustomerId of int

let Add1ToCustomerId (CustomerId i) = 
    CustomerId (i + 1)

let IsCustomerIdSmall (CustomerId i) = 
    i < 100000



//Use an obj wrapper
let ObjAdd1ToCustomerId (i: obj) = 
    (i :?> int + 1) :> obj

let ObjIsCustomerIdSmall (i: obj) = 
    (i :?> int) < 100000



//Use an alias
type AliasCustomerId = int
    
let AliasAdd1ToCustomerId (i: AliasCustomerId): AliasCustomerId = 
    (i + 1)
    
let AliasIsCustomerIdSmall (i: AliasCustomerId) = 
    i < 100000



//Use a struct
[<Struct>]
type StructCustomerId = StructCustomerId of int

let StructAdd1ToCustomerId (StructCustomerId i) = 
    StructCustomerId (i + 1)

let StructIsCustomerIdSmall (StructCustomerId i) = 
    i < 100000




printfn "F# wrapper classes are great for domain modeling ..."
#time
Array.init 10000000 CustomerId
// map it 
|> Array.map Add1ToCustomerId 
// map it again
|> Array.map Add1ToCustomerId 
// filter it 
|> Array.filter IsCustomerIdSmall 
|> ignore
#time

printfn "But the wrappers come with instantiation overhead, boxing, and GC"
#time
Array.init 10000000 (fun x -> x :> obj)
// map it 
|> Array.map ObjAdd1ToCustomerId 
// map it again
|> Array.map ObjAdd1ToCustomerId 
// filter it 
|> Array.filter ObjIsCustomerIdSmall 
|> ignore
#time

printfn "Aliasing is faster, but you lose the type safety"
#time
Array.init 10000000 (fun x -> x)
// map it 
|> Array.map AliasAdd1ToCustomerId 
// map it again
|> Array.map AliasAdd1ToCustomerId 
// filter it 
|> Array.filter AliasIsCustomerIdSmall 
|> ignore
#time

printfn "Why not just use structs?"
#time
Array.init 10000000 StructCustomerId
// map it 
|> Array.map StructAdd1ToCustomerId 
// map it again
|> Array.map StructAdd1ToCustomerId 
// filter it 
|> Array.filter StructIsCustomerIdSmall 
|> ignore
#time
