import { useEffect, useState } from "react"
import {
  Button,
  Modal,
  Tree,
  Col,
  Row,
  Input,
  notification,
  Checkbox,
} from "antd"
import {
  CheckCircleOutlined,
  ExclamationCircleOutlined,
} from "@ant-design/icons"

import makeRequest from "../../utils/makeRequest"
import {
  getCreatePermissionTreeData,
  getCreatedScopePermissionData,
} from "../../handle/createPermission"
import { requestUrl } from "../../resource/requestUrl"
import { dashboardRoute } from "../../config/dashboardRoute"

const CheckboxGroup = Checkbox.Group

const CreatePermissionModal = ({ visible, setVisible, scopes, setScopes }) => {
  const [permissions, setPermissions] = useState([])
  const [expandedKeys, setExpandKeys] = useState([])
  const [scopeName, setScopeName] = useState("")
  const [checkedPermissions, setCheckedPermissions] = useState([])
  const [checkedKeys, setCheckedKeys] = useState([])
  const [allowedRoutes, setAllowedRoutes] = useState([])

  useEffect(() => {
    const getPermissions = async () => {
      const data = await makeRequest({
        method: "GET",
        url: requestUrl.permission.readUrl(),
      })

      const { treeData, expandedKeys } = getCreatePermissionTreeData(data)
      setPermissions(treeData)
      setExpandKeys(expandedKeys)
    }

    getPermissions()
  }, [])

  const onAllowedRouteChange = (routes) => {
    setAllowedRoutes(routes)
  }

  const onCheck = (checkKeys, info) => {
    const permisisonActive = info.checkedNodes.filter(
      (item) => typeof item.key === "number"
    )
    setCheckedPermissions(permisisonActive)
    setCheckedKeys(checkKeys)
  }

  const onSavePermission = async () => {
    const createdScpData = getCreatedScopePermissionData(
      scopeName,
      checkedKeys,
      checkedPermissions
    )
    const rs = await makeRequest({
      url: requestUrl.scopePermission.createUrl(),
      method: "POST",
      data: { ...createdScpData, allowedRoute: JSON.stringify(allowedRoutes) },
    })

    let notificationData = {}
    notificationData.message = "Th??ng b??o"

    switch (rs.statusCode) {
      case 200:
        notificationData.icon = (
          <CheckCircleOutlined style={{ color: "#108ee9" }} />
        )
        notificationData.description = "L??u quy???n m???i th??nh c??ng"
        setScopes([...scopes, rs.data])
        break
      case 500:
        notificationData.icon = (
          <ExclamationCircleOutlined style={{ color: "#ff4d4f" }} />
        )
        notificationData.description = rs.data
        break
    }
    notification.open(notificationData)
    setVisible(false)
  }

  return (
    <Modal
      title="Th??m nh??m quy???n m???i"
      visible={visible}
      onOk={() => setVisible(false)}
      onCancel={() => setVisible(false)}
      width={1000}
      style={{ top: 20 }}
      footer={[
        <Button key="back" onClick={() => setVisible(false)}>
          Hu???
        </Button>,
        <Button
          key="submit"
          type="primary"
          onClick={async () => {
            await onSavePermission()
          }}
        >
          L??u
        </Button>,
      ]}
    >
      <Row justify="space-around" style={{ height: 500 }}>
        <Col span={14}>
          <Tree
            checkable
            expandedKeys={expandedKeys}
            selectable={false}
            treeData={permissions}
            onCheck={onCheck}
            height={500}
          />
        </Col>
        <Col
          span={8}
          style={{ height: "100%", overflowY: "auto", padding: "0 10px" }}
          className="custom-scroll-bar"
        >
          <p>T??n nh??m quy???n</p>
          <Input
            placeholder="T??n nh??m quy???n ghi li???n kh??ng d???u"
            onChange={(e) => setScopeName(e.target.value)}
            style={{ marginBottom: 15 }}
          />
          <p>Danh s??ch c??c m??n h??nh ???????c truy c???p</p>
          <CheckboxGroup
            options={dashboardRoute}
            value={allowedRoutes}
            onChange={onAllowedRouteChange}
          />
        </Col>
      </Row>
    </Modal>
  )
}

export default CreatePermissionModal
