import {useEffect, useState} from "react";
import type {ProductDto} from "../Api.ts";
import {api} from "@/apiClient.ts";
import toast from "react-hot-toast";
import {LogoutButton} from "@/components/LogoutButton.tsx";
import {useNavigate} from "react-router";

export  function DashboardPage() {

    const [products, setProducts] = useState<ProductDto[]>([]);
    const navigate = useNavigate();
    useEffect(() => {
        api.mine.productGetMyProducts()

            .then(response => {
                setProducts(response.data);

            })
            .catch(error => {
                console.log(error);
                toast.error("Could not load your listings");
            });


    }, []);
    return (
        <div>
            <button onClick={() => navigate("/create-listing")}>Create Listing</button>
            <h1>My Listings</h1>

            {products.map(product => (
                <div key={product.productId}>
                    <h2>{product.productName}</h2>
                    <p>Price: {product.productPrice}</p>
                    <p>Inventory: {product.inventory}</p>
                    <p>
                        Category: {product.category?.categoryName}
                    </p>
                </div>
            ))}

            <LogoutButton/>
        </div>
    );
}
