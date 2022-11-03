import React, { useEffect, useState } from "react";
import { getAllUserProducts } from "../../modules/productManager";
import { UserProduct } from "./UserProduct";
import './UserProduct.css'

export const UserProductList = () => {
    const [userProducts, setUserProducts] = useState([]);

    const getUserProducts = () => {
        getAllUserProducts().then(up => setUserProducts(up));
    }
    useEffect(() => {
        getUserProducts();
    }, []);

    const userProduct = true
    return (
        <section>
            <div className="myProducts-header">
                <p>My Products</p>
            </div>

            <div className="myProductList">
                <div className="row ">
                    {userProducts.map((u) => (
                        <UserProduct key={u.id} product={u.product} userProduct={userProduct} userProductId={u.id} getUserProducts={getUserProducts} />
                    ))}
                </div>
            </div>
        </section>
    );
}
