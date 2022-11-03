import { useEffect, useState } from "react"
import { useNavigate, useParams } from "react-router-dom"
import { Button } from "reactstrap"
import { getAllProducts, getProductByTypeId } from "../../modules/productManager"
import { Product } from "./Product"

export const ProductList = ({ isAdmin, isApproved, searchTermState }) => {
    const [products, setProducts] = useState([])
    const [filteredProducts, setFiltered] = useState([])
    const navigate = useNavigate()
    const getProducts = () => {
        getAllProducts().then(p => setProducts(p))
    }
    // const getProductsByType = () => {
    //     getProductByTypeId(typeId).then(p => setFiltered(p))
    // }
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
    return (<>

        <div className="container-p">

            {filteredProducts.map((product) => (
                <Product product={product} key={product.id} isAdmin={isAdmin} userProduct={userProduct} />
            ))}


        </div>
    </>
    )
}