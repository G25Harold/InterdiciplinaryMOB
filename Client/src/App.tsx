
import "./index.css";

import {Api, type ProductDto} from "../Api.ts";
import {useEffect, useState} from "react";

const MyApi= new Api();
export function App() {

    const [ products, setProducts ] = useState<ProductDto[]>([])

    useEffect(() => {
        MyApi.getProducts.productGetProducts().then (r  => {
            const data   = r.data;
            setProducts(data)
            const p= data[0]!;
            p.category?.categoryId

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
