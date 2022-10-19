export const ProductIngredient = ({ productIngredient }) => {

    return <>

        <tr>
            <td>{productIngredient.ingredient.name}</td>
            <td>{productIngredient.use}</td>
            <td>{productIngredient.ingredient.safetyInfo}</td>
        </tr>
    </>
}