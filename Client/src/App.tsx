
import "./index.css";

import {Api, type Product} from "../Api.ts";
import {useEffect, useState} from "react";

const MyApi= new Api();
export function App() {

    const [ products, setProducts ] = useState<Product[]>([])

    useEffect(() => {
        MyApi.getProducts.productGetProducts().then (r  => {
            const data   = r.data;
            setProducts(data)
        })
    },[] );
    
  return (
    <div className="app">
        {
       products.map(p => {
           return <div key={p.productId}>{p.productName} </div>
       })
        }
    </div>
  );
}

export default App;
