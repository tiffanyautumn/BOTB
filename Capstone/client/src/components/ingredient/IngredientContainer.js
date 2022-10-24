import { useState } from "react"
import { IngredientList } from "./IngredientList"
import { IngredientSearch } from "./IngredientSearch"

export const IngredientContainer = ({ isAdmin, isApproved }) => {
    const [searchTerms, setSearchTerms] = useState("")
    return <>
        <IngredientSearch setterFunction={setSearchTerms} />
        <IngredientList searchTermState={searchTerms} isAdmin={isAdmin} isApproved={isApproved} />
    </>
}