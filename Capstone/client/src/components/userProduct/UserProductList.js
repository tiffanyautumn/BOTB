import React, { useEffect, useState } from "react";
import { getAllUserProducts } from "../../modules/productManager";
import { UserProduct } from "./UserProduct";

export const UserProductList = () => {
    const [userProducts, setUserProducts] = useState([]);

    useEffect(() => {
        getAllUserProducts().then(setUserProducts);
    }, []);

    const userProduct = true
    return (
        <section>
            <div className="row ">
                {userProducts.map((u) => (
                    <UserProduct key={u.id} product={u.product} userProduct={userProduct} userProductId={u.id} getAllUserProducts={getAllUserProducts} />
                ))}
            </div>
        </section>
    );
}
