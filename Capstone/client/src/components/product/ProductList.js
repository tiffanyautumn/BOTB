import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { getAllProducts } from "../../modules/productManager"
import { Product } from "./Product"

export const ProductList = () => {
    const [products, setProducts] = useState([])
    const navigate = useNavigate()

    const getProducts = () => {
        getAllProducts().then(p => setProducts(p))
    }

    useEffect(() => {
        getProducts();
    }, []);


    return (
        <div className="container">
            <Button onClick={() => navigate('/product/create')}>Create a Product</Button>
            <div className="row justify-content-center">
                {products.map((product) => (
                    <Product product={product} key={product.id} />
                ))}
            </div>
        </div>
    )
}