import { useEffect, useState } from "react"
import { Button, Card, CardBody, FormGroup, Input, Label, Form, CardText } from "reactstrap"
import { editProductIngredient, getProductIngredientById } from "../../modules/productIngredientManager"

export const ProductIngredientEdit = ({ ProductIngredient, setFormActive, getProduct }) => {

    const [productIngredient, updateProductIngredient] = useState({
        active: false,
        use: ""
    })


    const getProductIngredient = () => {
        getProductIngredientById(ProductIngredient.id).then(i => updateProductIngredient(i))
    }

    useEffect(
        () => {
            getProductIngredient()
        }, []
    )

    const handleSaveButtonClick = (event) => {
        event.preventDefault()

        if (productIngredient.use === "") {
            productIngredient.use = null
        }


        const productIngredientToSend = {
            ingredientId: productIngredient.ingredientId,
            productId: productIngredient.productId,
            active: productIngredient.active,
            use: productIngredient.use,
            id: productIngredient.id
        }

        return editProductIngredient(productIngredient.id, productIngredientToSend)
            .then(() => {
                setFormActive(false)
                getProduct()
            })
    }
    return (<>
        <Card>
            <CardBody>
                <CardText>{productIngredient?.ingredient?.name}</CardText>
                <Form className="ingredientForm">
                    <FormGroup>
                        <Label for="active">Active</Label>
                        <Input
                            id="active"
                            name="active"
                            type="checkbox"
                            value={productIngredient.active}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.active = evt.target.checked
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>
                    <FormGroup>
                        <Label for="use">Use</Label>
                        <Input
                            id="use"
                            name="use"
                            type="text"
                            value={productIngredient.use}
                            onChange={
                                (evt) => {
                                    const copy = { ...productIngredient }
                                    copy.use = evt.target.value
                                    updateProductIngredient(copy)
                                }
                            } />
                    </FormGroup>

                    <Button onClick={(clickEvent) => handleSaveButtonClick(clickEvent)}
                        className="btn btn-primary">
                        Save
                    </Button>
                </Form>
                <Button onClick={() => setFormActive(false)}>Cancel</Button>
            </CardBody>
        </Card>

    </>)
}