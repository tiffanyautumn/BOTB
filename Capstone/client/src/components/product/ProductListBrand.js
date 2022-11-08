import { useEffect, useState } from "react"
import { useParams } from "react-router-dom"
import { getBrandById } from "../../modules/brandManager"
import { getProductByBrandId } from "../../modules/productManager"
import { Product } from "./Product"

export const ProductListBrand = () => {
    const [products, setProducts] = useState([])
    const [brand, setBrand] = useState([])
    const { brandId } = useParams()

    const getProducts = () => {
        getProductByBrandId(brandId).then(p => setProducts(p))
    }

    const getBrand = () => {
        getBrandById(brandId).then(b => setBrand(b))
    }

    useEffect(() => {
        getProducts();
        getBrand();
    }, []);

    return <>
        <div className="pl-section">
            <section className="products-header">
                <h3>{brand.name} Products</h3>
            </section>

            <div className="container-p">
                {products.map((p) => {
                    return <div key={p.id} ><Product key={p.id} product={p} /></div>
                })
                }
            </div>
        </div>
    </>
}