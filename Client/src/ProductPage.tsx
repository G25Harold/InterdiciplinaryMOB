import {useEffect, useState} from "react";
import {Api, type ProductDto} from "../Api.ts";
const MyApi= new Api();


export function ProductPage() {



        const [ products, setProducts ] = useState<ProductDto[]>([])

        useEffect(() => {
            MyApi.getProducts.productGetProducts({page: 1,
                resultsPerPage: 1}).then (r  => {
                const data   = r.data;
                setProducts(data)
                const p= data[0]!;
                p.category?.categoryId

            })
        },[] );

        function createProduct() {
            MyApi.createProduct.productCreateProduct()
        }

        return (
            <div className="app">
                {
                    products.map(p => {
                        return <div key={p.productId}>{p.productName} </div>
                    })
                }
                <button onClick={createProduct}>Create Product</button>
            </div>
        );
    }

    export default ProductPage;
