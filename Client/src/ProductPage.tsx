import {useEffect, useState} from "react";
import {type ProductDto} from "../Api.ts";
import {LogoutButton} from "@/components/LogoutButton.tsx";
import {api} from "@/apiClient.ts";


export function ProductPage() {



        const [ products, setProducts ] = useState<ProductDto[]>([])

        useEffect(() => {
            api.getProducts.productGetProducts({page: 1,
                resultsPerPage: 1}).then (r  => {
                const data   = r.data;
                setProducts(data)
                const p= data[0]!;
                p.category?.categoryId

            })
        },[] );

        function createProduct() {
            api.createProduct.productCreateProduct()
        }

        return (

            <div className="app">
                {
                    products.map(p => {
                        return <div key={p.productId}>{p.productName} </div>
                    })
                }
                <button onClick={createProduct}>Create Product</button>
                <LogoutButton/>
            </div>
        );
    }

    export default ProductPage;
