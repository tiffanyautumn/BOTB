import { useState } from "react"
import { ProductList } from "./ProductList"
import { ProductSearch } from "./ProductSearch"

export const ProductContainer = ({ isAdmin, isApproved }) => {
    const [searchTerms, setSearchTerms] = useState("")
    return <>
        <ProductSearch setterFunction={setSearchTerms} />
        <ProductList searchTermState={searchTerms} isAdmin={isAdmin} isApproved={isApproved} />
    </>
}