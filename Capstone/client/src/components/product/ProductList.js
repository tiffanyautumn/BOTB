import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { getAllProducts } from "../../modules/productManager"
import { Product } from "./Product"

export const ProductList = ({ isAdmin, isApproved, searchTermState }) => {
    const [products, setProducts] = useState([])
    const [filteredProducts, setFiltered] = useState([])
    const navigate = useNavigate()

    const getProducts = () => {
        getAllProducts().then(p => setProducts(p))
    }

    useEffect(() => {
        getProducts();
    }, []);

    useEffect(() => {
        getSearchedProducts(searchTermState);
    },
        [searchTermState]
    )

    const getSearchedProducts = (searchTerm) => {
        fetch(`/api/product/search?q=${searchTerm}`)
            .then(res => res.json())
            .then((p) => {
                setFiltered(p)
            })
    }
    const userProduct = false
    return (
        <div className="container">


            <div className="row ">
                {filteredProducts.map((product) => (
                    <Product product={product} key={product.id} isAdmin={isAdmin} userProduct={userProduct} />
                ))}
            </div>
            {
                isAdmin
                    ? <Button onClick={() => navigate('/product/create')}>Create a Product</Button>
                    : ""
            }
        </div>
    )
}