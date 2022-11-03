import { useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button } from "reactstrap"
import { ProductList } from "./ProductList"
import { ProductSearch } from "./ProductSearch"

export const ProductContainer = ({ isAdmin, isApproved }) => {
    const [searchTerms, setSearchTerms] = useState("")
    const navigate = useNavigate()
    return <>
        <section className="psection">
            <div className="products-header">
                <h3>Products </h3>

                <ProductSearch setSearchTerms={setSearchTerms} />
                {
                    isAdmin
                        ? <div className="createProductBtn"><button className="btn" onClick={() => navigate('/product/create')}>Create a Product</button></div>
                        : ""
                }
            </div>
        </section>
        <section className="productlist-section">
            <ProductList searchTermState={searchTerms} isAdmin={isAdmin} isApproved={isApproved} />
        </section>
    </>
}