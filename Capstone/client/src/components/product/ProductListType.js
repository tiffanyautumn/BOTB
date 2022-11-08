import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { getProductByBrandId, getProductByTypeId } from "../../modules/productManager"
import { getTypeById } from "../../modules/typeManager"
import { Product } from "./Product"
import './Product.css'

export const ProductListType = () => {
    const [products, setProducts] = useState([])
    const [type, setType] = useState([])
    const { typeId } = useParams()

    const getProducts = () => {
        getProductByTypeId(typeId).then(p => setProducts(p))
    }
    const getType = () => {
        getTypeById(typeId).then(t => setType(t))
    }



    useEffect(() => {
        getProducts();
        getType()
    }, []);

    return <>
        <div className="pl-section">
            <section className="products-header">
                <h3>{type.name} Products</h3>
            </section>


            <div className="container-p">
                {products.map((p) => {
                    return <Product key={p.id} product={p} />
                })
                }
            </div>
        </div>
    </>
}